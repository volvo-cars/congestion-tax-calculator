package com.congestiontaxcalculator.helpers;

import com.congestiontaxcalculator.models.Interval;
import com.congestiontaxcalculator.models.Vehicle;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import org.apache.logging.log4j.Level;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Service;

@Service
public class CongestionTaxCalculator {

    private static final Logger LOGGER = LogManager.getLogger(CongestionTaxCalculator.class);

    private static Map<String, Integer> tollFreeVehicles = new HashMap<>();
    private Interval[] intervals;


    public CongestionTaxCalculator() {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            intervals = objectMapper.readValue(new File("src/main/resources/interval.json"), Interval[].class);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    static {
        tollFreeVehicles.put("Motorcycle", 0);
        tollFreeVehicles.put("Tractor", 1);
        tollFreeVehicles.put("Emergency", 2);
        tollFreeVehicles.put("Diplomat", 3);
        tollFreeVehicles.put("Foreign", 4);
        tollFreeVehicles.put("Military", 5);
    }

    public int getTax(Vehicle vehicle, Date[] dates)
    {
        if (IsTollFreeVehicle(vehicle) || dates == null) {
            return 0;
        }
        Arrays.sort(dates);
        Date intervalStart = dates[0];
        int totalFee = 0;
        int totalDayFee = 0;
        int previousFee = 0;
        for (Date date : dates) {
            int currentFee = GetTollFee(date);
            if (date.getDay() == intervalStart.getDay()) {
                if (totalDayFee < 60) {
                    long diffInMillis = date.getTime() - intervalStart.getTime();
                    long minutes = diffInMillis / 1000 / 60;
                    if (minutes <= 60) {
                        if (currentFee > previousFee) {
                            totalDayFee -= previousFee;
                            totalDayFee += currentFee;
                            previousFee = currentFee;
                        }
                    } else {
                        intervalStart = date;
                        totalDayFee += currentFee;
                        previousFee = currentFee;
                    }
                }
            } else {
                intervalStart = date;
                totalFee += Math.min(totalDayFee, 60);
                totalDayFee = currentFee;
                previousFee = currentFee;
            }
        }
        totalFee += Math.min(totalDayFee, 60);
        return totalFee;
    }


    private boolean IsTollFreeVehicle(Vehicle vehicle) {
        if (vehicle == null) return false;
        String vehicleType = vehicle.getVehicleType();
        return tollFreeVehicles.containsKey(vehicleType);
    }

    public int GetTollFee(Date date)
    {
        int fee = 0;
        if (IsTollFreeDate(date)) return fee;
        int militaryTime = getHourMinAsInt(date);
        try {
            fee = Arrays.stream(intervals).filter(interval -> {
                if (interval.getEnd() > interval.getStart()) {
                    return interval.getStart() <= militaryTime && interval.getEnd() >= militaryTime;
                } else {
                    return interval.getStart() <= militaryTime || interval.getEnd() >= militaryTime;
                }
            }).findFirst().get().getValue();
        } catch (Exception ex) {
            LOGGER.log(Level.WARN, "No matching interval found returning 0 tax fee");
        }
        return fee;
    }

    private int getHourMinAsInt(Date date) {
        String minutes = "";
        if (date.getMinutes() < 10) {
            minutes = "0"+date.getMinutes();
        } else {
            minutes = "" + date.getMinutes();
        }
        return Integer.parseInt (date.getHours() + minutes );
    }

    private Boolean IsTollFreeDate(Date date)
    {
        // I would replace date with using LocalDateTime or ZonedDateTime to solve the issue of getYear returning wrong value, and because the methods are deprecated.
        // but would have to write a custom string to object mapper as the @JsonFormat do not support it..
        int year = date.getYear();
        int month = date.getMonth() + 1;
        int day = date.getDay() + 1;
        int dayOfMonth = date.getDate();

        if (day == Calendar.SATURDAY || day == Calendar.SUNDAY) return true;

        if (year == 113) {
            return (month == 1 && dayOfMonth == 1) ||
                    (month == 3 && (dayOfMonth == 28 || dayOfMonth == 29)) ||
                    (month == 4 && (dayOfMonth == 1 || dayOfMonth == 30)) ||
                    (month == 5 && (dayOfMonth == 1 || dayOfMonth == 8 || dayOfMonth == 9)) ||
                    (month == 6 && (dayOfMonth == 5 || dayOfMonth == 6 || dayOfMonth == 21)) ||
                    (month == 7) ||
                    (month == 11 && dayOfMonth == 1) ||
                    (month == 12 && (dayOfMonth == 24 || dayOfMonth == 25 || dayOfMonth == 26 || dayOfMonth == 31));
        }
        return false;
    }
}
