import Vehicle from "./vehicle";

export default class Motorbike implements Vehicle {
    getVehicleType(): string {
        return "Motorbike";
    }
}