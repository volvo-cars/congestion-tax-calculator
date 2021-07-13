package com.congestiontaxcalculator.models;

public class Emergency implements Vehicle{

    @Override
    public String getVehicleType() {
        return "Emergency";
    }
}
