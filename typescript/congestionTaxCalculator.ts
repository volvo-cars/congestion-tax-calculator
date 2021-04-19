import Vehicle from "./vehicle";

enum TollFreeVehicles {
	Motorcycle,
	Tractor,
	Emergency,
	Diplomat,
	Foreign,
	Military
  }

function getTax(vehicle: Vehicle, dates: Date[]): number {
    let intervalStart: Date = dates[0];
    let totalFee: number = 0;

    for (let i = 0; i < dates.length; i++) {
        const date: Date = dates[i];
        let nextFee: number = getTollFee(date, vehicle);
        let tempFee = getTollFee(intervalStart, vehicle);

        let diffInMillies = date.getTime() - intervalStart.getTime();
        let minutes = diffInMillies/1000/60;

        if (minutes <= 60) {
            if (totalFee > 0) totalFee -= tempFee;
            if (nextFee >= tempFee) tempFee = nextFee;
            totalFee += tempFee;
        } else {
            totalFee += nextFee;
        }

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    return 0;
}

function isTollFreeVehicle(vehicle: Vehicle): boolean {
    if (vehicle == null) return false;
    const vehicleType: string = vehicle.getVehicleType();

    return vehicleType == TollFreeVehicles[TollFreeVehicles.Motorcycle] ||
           vehicleType == TollFreeVehicles[TollFreeVehicles.Tractor] ||
           vehicleType == TollFreeVehicles[TollFreeVehicles.Emergency] ||
           vehicleType == TollFreeVehicles[TollFreeVehicles.Diplomat] ||
           vehicleType == TollFreeVehicles[TollFreeVehicles.Foreign] ||
           vehicleType == TollFreeVehicles[TollFreeVehicles.Military];
}

function getTollFee(date: Date, vechicle: Vehicle): number {
    if(isTollFreeDate(date) || isTollFreeVehicle(vechicle)) return 0;

    const hour:number = date.getHours();
    const minute:number = date.getMinutes();

    if (hour == 6 && minute >= 0 && minute <= 29) return 8;
    else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
    else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
    else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
    else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
    else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
    else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
    else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
    else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
    else return 0;
}

function isTollFreeDate(date: Date): boolean {
    const year:number = date.getFullYear();
    const month:number = date.getMonth() + 1;
    const day:number = date.getDay() + 1;
    const dayOfMonth:number = date.getDate();

    if (day == 6 || day == 0) return true;

    if (year == 2013)
        {
            if ((month == 1 && dayOfMonth == 1) ||
                    (month == 3 && (dayOfMonth == 28 || dayOfMonth == 29)) ||
                    (month == 4 && (dayOfMonth == 1 || dayOfMonth == 30)) ||
                    (month == 5 && (dayOfMonth == 1 || dayOfMonth == 8 || dayOfMonth == 9)) ||
                    (month == 6 && (dayOfMonth == 5 || dayOfMonth == 6 || dayOfMonth == 21)) ||
                    (month == 7) ||
                    (month == 11 && dayOfMonth == 1) ||
                    (month == 12 && (dayOfMonth == 24 || dayOfMonth == 25 || dayOfMonth == 26 || dayOfMonth == 31)))
            {
                return true;
            }
        }
        return false;
}