﻿namespace KitchenStorage.ViewModel;

public record InventoryViewModel
    (Guid? Id, string Name, double Value, double AlertLimit, string Description, string CreateDate, double Status, MeasurementTypeViewModel? Type);

public record UpsertInventoryViewModel
    (Guid? Id, string Name, double Value, double AlertLimit, Guid TypeId, string Description);

public enum InventoryActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2
}
public enum InventoryType
{
    LogIn = 0,
    LogOut = 1
}
