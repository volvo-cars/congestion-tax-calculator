package com.congestiontaxcalculator.controller;


import com.congestiontaxcalculator.helpers.CongestionTaxCalculator;
import com.congestiontaxcalculator.models.CalculateTaxRequest;
import com.congestiontaxcalculator.models.CalculateTaxResponse;
import com.congestiontaxcalculator.models.Car;
import com.congestiontaxcalculator.models.Diplomat;
import com.congestiontaxcalculator.models.Emergency;
import com.congestiontaxcalculator.models.Foreign;
import com.congestiontaxcalculator.models.Military;
import com.congestiontaxcalculator.models.Vehicle;
import com.fasterxml.jackson.databind.ObjectMapper;
import javax.ws.rs.BadRequestException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/congestion/tax")
public class CongestionTaxCalculatorV1Controller {

    @Autowired
    CongestionTaxCalculator congestionTaxCalculator;

    @PutMapping
    @RequestMapping("/{vehicleType}")
    public String getTax(@PathVariable String vehicleType, @RequestBody String request) {
        ObjectMapper objectMapper = new ObjectMapper();
        CalculateTaxRequest calculateTaxRequest = null;
        try {
            calculateTaxRequest = objectMapper.readValue(request, CalculateTaxRequest.class);
        } catch (Exception e) {
            throw new BadRequestException("Error parsing the Request body to CalculateTaxRequest");
        }
        Vehicle vehicle = getVehicle(vehicleType);
        if (vehicle == null) {
            // For such cases a custom and proper Error handling can be written to cover all possible error messages for the application.
            return("provided vehicleType \"" + vehicleType + "\" is not supported");
        }
        return new CalculateTaxResponse().setTaxAmount(congestionTaxCalculator.getTax(vehicle, calculateTaxRequest.getDates())).toString();
    }

    private Vehicle getVehicle(String vehicleType) {
        Vehicle vehicle = null;
        switch (vehicleType) {
            case "Diplomat":
                vehicle = new Diplomat();
                break;
            case "Emergency":
                vehicle = new Emergency();
                break;
            case "Foreign":
                vehicle = new Foreign();
                break;
            case "Military":
                vehicle = new Military();
                break;
            case "Car":
                vehicle = new Car();
                break;
        }
        return vehicle;
    }

}
