package com.congestiontaxcalculator;

import java.nio.file.Files;
import java.nio.file.Paths;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.MOCK,classes = CongestionTaxCalculatorApplication.class)
@RunWith(SpringRunner.class)
@AutoConfigureMockMvc
class CongestionTaxCalculatorApplicationTests {

    @Autowired
    private MockMvc mockMvc;

    @Test
    void getTaxForOneDaySuccess() throws Exception {
        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.put("/api/v1/congestion/tax/Car")
                .accept(MediaType.APPLICATION_JSON).contentType(MediaType.APPLICATION_JSON)
                .content(Files.readAllBytes(Paths.get("src/test/resources/request-50.json"))));

        Assertions.assertEquals("{\"taxAmount\": 50}", response.andReturn().getResponse().getContentAsString());
    }

    @Test
    void getTaxForMultiDaySuccess() throws Exception {
        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.put("/api/v1/congestion/tax/Car")
                .accept(MediaType.APPLICATION_JSON).contentType(MediaType.APPLICATION_JSON)
                .content(Files.readAllBytes(Paths.get("src/test/resources/request-multiday-89.json"))));

        Assertions.assertEquals("{\"taxAmount\": 89}", response.andReturn().getResponse().getContentAsString());
    }

    @Test
    void getTaxBadRequestException() throws Exception {
        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.put("/api/v1/congestion/tax/Car")
                .accept(MediaType.APPLICATION_JSON).contentType(MediaType.APPLICATION_JSON));

        Assertions.assertEquals(
                "Required request body is missing: public java.lang.String com.congestiontaxcalculator.controller.CongestionTaxCalculatorV1Controller.getTax(java.lang.String,java.lang.String)",
                response.andReturn().getResolvedException().getMessage());
    }

    @Test
    void getTaxNotSupportedVehicle() throws Exception {
        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.put("/api/v1/congestion/tax/Fish")
                .accept(MediaType.APPLICATION_JSON).contentType(MediaType.APPLICATION_JSON)
                .content(Files.readAllBytes(Paths.get("src/test/resources/request-multiday-89.json"))));

        Assertions.assertEquals(
                "provided vehicleType \"Fish\" is not supported",
                response.andReturn().getResponse().getContentAsString());
    }
}
