<template>
  <v-col cols="12">
    <v-form ref="coffeeShopForm">
      <v-row>
        <v-col cols="12" md="6" sm="6">
          <v-text-field
            outlined
            label="نام"
            placeholder="نام"
            clearable
            class="rounded-lg"
            v-model="coffeeShop.name"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12" md="6" sm="6">
          <v-text-field
            outlined
            label="عنوان"
            placeholder="عنوان"
            clearable
            class="rounded-lg"
            v-model="coffeeShop.title"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <v-select
            outlined
            placeholder="نوع کافی شاپ"
            label="نوع کافی شاپ"
            item-text="title"
            item-value="id"
            clearable
            multiple
            class="rounded-lg"
            no-data-text="موردی یافت نشد"
            :items="types"
            v-model="coffeeShop.types"
            :rules="[rules.requireSelect]"
          ></v-select>
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <v-select
            outlined
            placeholder="گروه ها"
            label="گروه ها"
            item-text="title"
            item-value="id"
            clearable
            multiple
            class="rounded-lg"
            no-data-text="موردی یافت نشد"
            :items="groups"
            v-model="coffeeShop.groups"
            :rules="[rules.requireSelect]"
          ></v-select>
        </v-col>
        <v-col cols="12">
          <v-textarea
            outlined
            label="درباره"
            placeholder="درباره کافه"
            clearable
            class="rounded-lg"
            auto-grow
            v-model="coffeeShop.about"
          />
        </v-col>
        <v-col cols="12">
          <v-text-field
            outlined
            label="آدرس"
            placeholder="آدرس"
            clearable
            class="rounded-lg"
            v-model="coffeeShop.address"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12">
          <v-card elevation="5" outlined>
            <l-map
              style="height: 400px"
              :zoom="map.zoom"
              :center="map.center"
              :options="{ attributionControl: false, zoomControl: false }"
            >
              <l-tile-layer :url="map.url"></l-tile-layer>
              <l-marker draggable :lat-lng.sync="markerLatLng" :icon="map.icon">
              </l-marker>
            </l-map>
          </v-card>
        </v-col>
        <v-col cols="12">
          <v-btn block class="rounded-lg" elevation="5" color="primary" @click="upsert">
            ثبت کافی شاپ
          </v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-col>
</template>

<script lang="ts">
import L from "leaflet";
import {
  LMap,
  LTileLayer,
  LMarker,
  LPopup,
  LIcon,
  LCircle,
} from "vue2-leaflet";
import "leaflet/dist/leaflet.css";

import { storesTypes } from "@/api/apis/coffeeShop.api";
import { getGroups } from "@/api/apis/group.apis";
import Vue from "vue";
import { currentLocation } from "@/services/location";
import { mapMutations } from "vuex";
import { SNACKBAR } from "@/store/store_types";
import { rules } from "@/constants";
export default Vue.extend({
  components: {
    LMap,
    LTileLayer,
    LMarker,
    LPopup,
    LIcon,
    LCircle,
  },
  data: () => ({
    rules:rules,
    types: [] as Array<any>,
    groups: [] as Array<any>,
    coffeeShop: {
      name: "",
      title: "",
      about: "",
      address: "",
      lat: 0,
      lon: 0,
      groups: [] as Array<any>,
      types: [] as Array<any>,
    },
    markerLatLng: [0, 0],
    map: {
      url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
      zoom: 15,
      center: [0, 0],
      currentLoc: [0, 0],
      iconSize: 64,
      icon: L.icon({
        iconUrl:
          "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAApCAYAAADAk4LOAAAFgUlEQVR4Aa1XA5BjWRTN2oW17d3YaZtr2962HUzbDNpjszW24mRt28p47v7zq/bXZtrp/lWnXr337j3nPCe85NcypgSFdugCpW5YoDAMRaIMqRi6aKq5E3YqDQO3qAwjVWrD8Ncq/RBpykd8oZUb/kaJutow8r1aP9II0WmLKLIsJyv1w/kqw9Ch2MYdB++12Onxee/QMwvf4/Dk/Lfp/i4nxTXtOoQ4pW5Aj7wpici1A9erdAN2OH64x8OSP9j3Ft3b7aWkTg/Fm91siTra0f9on5sQr9INejH6CUUUpavjFNq1B+Oadhxmnfa8RfEmN8VNAsQhPqF55xHkMzz3jSmChWU6f7/XZKNH+9+hBLOHYozuKQPxyMPUKkrX/K0uWnfFaJGS1QPRtZsOPtr3NsW0uyh6NNCOkU3Yz+bXbT3I8G3xE5EXLXtCXbbqwCO9zPQYPRTZ5vIDXD7U+w7rFDEoUUf7ibHIR4y6bLVPXrz8JVZEql13trxwue/uDivd3fkWRbS6/IA2bID4uk0UpF1N8qLlbBlXs4Ee7HLTfV1j54APvODnSfOWBqtKVvjgLKzF5YdEk5ewRkGlK0i33Eofffc7HT56jD7/6U+qH3Cx7SBLNntH5YIPvODnyfIXZYRVDPqgHtLs5ABHD3YzLuespb7t79FY34DjMwrVrcTuwlT55YMPvOBnRrJ4VXTdNnYug5ucHLBjEpt30701A3Ts+HEa73u6dT3FNWwflY86eMHPk+Yu+i6pzUpRrW7SNDg5JHR4KapmM5Wv2E8Tfcb1HoqqHMHU+uWDD7zg54mz5/2BSnizi9T1Dg4QQXLToGNCkb6tb1NU+QAlGr1++eADrzhn/u8Q2YZhQVlZ5+CAOtqfbhmaUCS1ezNFVm2imDbPmPng5wmz+gwh+oHDce0eUtQ6OGDIyR0uUhUsoO3vfDmmgOezH0mZN59x7MBi++WDL1g/eEiU3avlidO671bkLfwbw5XV2P8Pzo0ydy4t2/0eu33xYSOMOD8hTf4CrBtGMSoXfPLchX+J0ruSePw3LZeK0juPJbYzrhkH0io7B3k164hiGvawhOKMLkrQLyVpZg8rHFW7E2uHOL888IBPlNZ1FPzstSJM694fWr6RwpvcJK60+0HCILTBzZLFNdtAzJaohze60T8qBzyh5ZuOg5e7uwQppofEmf2++DYvmySqGBuKaicF1blQjhuHdvCIMvp8whTTfZzI7RldpwtSzL+F1+wkdZ2TBOW2gIF88PBTzD/gpeREAMEbxnJcaJHNHrpzji0gQCS6hdkEeYt9DF/2qPcEC8RM28Hwmr3sdNyht00byAut2k3gufWNtgtOEOFGUwcXWNDbdNbpgBGxEvKkOQsxivJx33iow0Vw5S6SVTrpVq11ysA2Rp7gTfPfktc6zhtXBBC+adRLshf6sG2RfHPZ5EAc4sVZ83yCN00Fk/4kggu40ZTvIEm5g24qtU4KjBrx/BTTH8ifVASAG7gKrnWxJDcU7x8X6Ecczhm3o6YicvsLXWfh3Ch1W0k8x0nXF+0fFxgt4phz8QvypiwCCFKMqXCnqXExjq10beH+UUA7+nG6mdG/Pu0f3LgFcGrl2s0kNNjpmoJ9o4B29CMO8dMT4Q5ox8uitF6fqsrJOr8qnwNbRzv6hSnG5wP+64C7h9lp30hKNtKdWjtdkbuPA19nJ7Tz3zR/ibgARbhb4AlhavcBebmTHcFl2fvYEnW0ox9xMxKBS8btJ+KiEbq9zA4RthQXDhPa0T9TEe69gWupwc6uBUphquXgf+/FrIjweHQS4/pduMe5ERUMHUd9xv8ZR98CxkS4F2n3EUrUZ10EYNw7BWm9x1GiPssi3GgiGRDKWRYZfXlON+dfNbM+GgIwYdwAAAAASUVORK5CYII=",
        iconSize: [32, 37],
        iconAnchor: [16, 37],
      }),
    },
  }),
  mounted() {
    this.getTypes();
    this.getGroups();
    this.setLoaction();
  },
  watch: {
    markerLatLng(location) {
      this.findAndSetAddress(location);
    },
  },
  methods: {
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    getTypes() {
      storesTypes().then((typesRes) => {
        if (typesRes.status) {
          this.types = typesRes.result;
        }
      });
    },
    getGroups() {
      getGroups(0, 0).then((groupsRes) => {
        if (groupsRes.status) {
          this.groups = groupsRes.result.groups;
        }
      });
    },
    upsert(){
        if((this.$refs.coffeeShopForm as any).validate()){
          console.log('valid');
          
        }
    },
    async setLoaction() {
      let lat, lon;

      // Find location and set it
      try {
        let loc = await currentLocation();

        lat = loc.coords.latitude;
        lon = loc.coords.longitude;
      } catch {
        this.showSnackbar("مکان شما شناسایی نشد");
        lat = 35.71089;
        lon = 51.334968;
      }

      // Set map location
      this.map.center = [lat, lon];
      this.map.currentLoc = [lat,lon];
      this.markerLatLng = [lat, lon];
    },
    findAndSetAddress(location: any) {
      // Set Coffee shop location
      this.coffeeShop.lat = location.lat;
      this.coffeeShop.lon = location.lng;

      console.log(location);
    },
  },
});
</script>