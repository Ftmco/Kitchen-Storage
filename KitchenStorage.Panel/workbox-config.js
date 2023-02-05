module.exports = {
	globDirectory: 'src/',
	globPatterns: [
		'**/*.{ts,vue,png,jpg,svg,css}'
	],
	swDest: 'src/sw.js',
	ignoreURLParametersMatching: [
		/^utm_/,
		/^fbclid$/,
		/^cs/
	]
};