from datetime import datetime
from enum import Enum
from vehicle import Vehicle


def get_tax(vehicle: Vehicle, dates: list):
    interval_start = dates[0]
    total_fee = 0
    for date in dates:
        next_fee = get_toll_fee(date, vehicle)
        temp_fee = get_toll_fee(interval_start, vehicle)

        diff_in_seconds = date.timestamp() - interval_start.timestamp()
        minutes = diff_in_seconds / 60

        if minutes <= 60:
            if total_fee > 0:
                total_fee = total_fee - temp_fee
            if next_fee >= temp_fee:
                temp_fee = next_fee
            total_fee = total_fee + temp_fee
        else:
            total_fee = total_fee + next_fee
    if total_fee > 60:
        total_fee = 60
    return total_fee


def is_toll_free_vehicle(vehicle: Vehicle) -> bool:
    if vehicle == None:
        return False

    vehicle_type = vehicle.get_vehicle_type(vehicle)
    return vehicle_type == TollFreeVehicles.MOTORCYCLE.name.capitalize() or vehicle_type == TollFreeVehicles.TRACTOR.name.capitalize() or vehicle_type == TollFreeVehicles.EMERGENCY.name.capitalize() or vehicle_type == TollFreeVehicles.DIPLOMAT.name.capitalize() or vehicle_type == TollFreeVehicles.FOREIGN.name.capitalize() or vehicle_type == TollFreeVehicles.MILITARY.name.capitalize()


def get_toll_fee(date: datetime, vehicle: Vehicle) -> int:
    if is_toll_free_date(date) or is_toll_free_vehicle(vehicle):
        return 0

    hour = date.hour
    minute = date.minute

    if hour == 6 and minute >= 0 or minute <= 29:
        return 8
    elif hour == 6 and minute >= 30 and minute <= 59:
        return 13
    elif hour == 7 and minute >= 0 and minute <= 59:
        return 18
    elif hour == 8 and minute >= 0 and minute <= 29:
        return 13
    elif hour >= 8 and hour <= 14 and minute >= 30 and minute <= 59:
        return 8
    elif hour == 15 and minute >= 0 and minute <= 29:
        return 13
    elif hour == 15 and minute >= 0 or hour == 16 and minute <= 59:
        return 18
    elif hour == 17 and minute >= 0 and minute <= 59:
        return 13
    elif hour == 18 and minute >= 0 and minute <= 29:
        return 8
    else:
        return 0


def is_toll_free_date(date: datetime):
    year = date.year
    month = date.month
    day = date.day

    if date.weekday() == 5 or date.weekday() == 6:
        return True

    if year == 2013:
        if month == 1 and day == 1 or month == 3 and (day == 28 or day == 29) or month == 4 and (day == 1 or day == 30) or month == 5 and (day == 1 or day == 8 or day == 9) or month == 6 and (day == 5 or day == 6 or day == 21) or month == 7 or month == 11 and day == 1 or month == 12 and (day == 24 or day == 25 or day == 26 or day == 31):
            return True

    return False


class TollFreeVehicles(Enum):
    MOTORCYCLE = 1
    TRACTOR = 2
    EMERGENCY = 3
    DIPLOMAT = 4
    FOREIGN = 5
    MILITARY = 6
