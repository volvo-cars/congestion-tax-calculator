package com.congestiontaxcalculator.models;

public class CalculateTaxResponse {
    private Integer taxAmount;

    public CalculateTaxResponse setTaxAmount(Integer taxAmount) {
        this.taxAmount = taxAmount;
        return this;
    }

    @Override
    public String toString() {
        return "{\"taxAmount\": " + taxAmount +"}";
    }
}
