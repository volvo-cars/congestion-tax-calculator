package congestion.calculator

import java.util.Calendar
import java.util.Date

class CongestionTaxCalculator {
    fun getTax(vehicle: Vehicle?, dates: Array<Date>): Int {
        val intervalStart = dates[0]
        var totalFee = 0

        for (i in dates.indices) {
            val date = dates[i]
            val nextFee = GetTollFee(date, vehicle)
            var tempFee = GetTollFee(intervalStart, vehicle)

            val diffInMillies = date.time - intervalStart.time
            val minutes = diffInMillies / 1000 / 60

            if (minutes <= 60) {
                if (totalFee > 0) totalFee -= tempFee
                if (nextFee >= tempFee) tempFee = nextFee
                totalFee += tempFee
            } else {
                totalFee += nextFee
            }
        }

        if (totalFee > 60) totalFee = 60
        return totalFee
    }

    private fun IsTollFreeVehicle(vehicle: Vehicle?): Boolean {
        if (vehicle == null) return false
        val vehicleType = vehicle.vehicleType
        return tollFreeVehicles.containsKey(vehicleType)
    }

    fun GetTollFee(date: Date, vehicle: Vehicle?): Int {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0

        val hour = date.hours
        val minute = date.minutes

        return if (hour == 6 && minute >= 0 && minute <= 29) 8
        else if (hour == 6 && minute >= 30 && minute <= 59) 13
        else if (hour == 7 && minute >= 0 && minute <= 59) 18
        else if (hour == 8 && minute >= 0 && minute <= 29) 13
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) 8
        else if (hour == 15 && minute >= 0 && minute <= 29) 13
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) 18
        else if (hour == 17 && minute >= 0 && minute <= 59) 13
        else if (hour == 18 && minute >= 0 && minute <= 29) 8
        else 0
    }

    private fun IsTollFreeDate(date: Date): Boolean {
        val year = date.year
        val month = date.month + 1
        val day = date.day + 1
        val dayOfMonth = date.date

        if (day == Calendar.SATURDAY || day == Calendar.SUNDAY) return true

        if (year == 2013) {
            if ((month == 1 && dayOfMonth == 1) ||
                (month == 3 && (dayOfMonth == 28 || dayOfMonth == 29)) ||
                (month == 4 && (dayOfMonth == 1 || dayOfMonth == 30)) ||
                (month == 5 && (dayOfMonth == 1 || dayOfMonth == 8 || dayOfMonth == 9)) ||
                (month == 6 && (dayOfMonth == 5 || dayOfMonth == 6 || dayOfMonth == 21)) ||
                (month == 7) ||
                (month == 11 && dayOfMonth == 1) ||
                (month == 12 && (dayOfMonth == 24 || dayOfMonth == 25 || dayOfMonth == 26 || dayOfMonth == 31))
            ) {
                return true
            }
        }
        return false
    }

    companion object {
        private val tollFreeVehicles: MutableMap<String?, Int> = HashMap()

        init {
            tollFreeVehicles["Motorcycle"] = 0
            tollFreeVehicles["Tractor"] = 1
            tollFreeVehicles["Emergency"] = 2
            tollFreeVehicles["Diplomat"] = 3
            tollFreeVehicles["Foreign"] = 4
            tollFreeVehicles["Military"] = 5
        }
    }
}
