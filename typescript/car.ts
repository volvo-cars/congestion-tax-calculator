import Vehicle from "./vehicle";

export class Car implements Vehicle {
    getVehicleType(): string {
        return "Car";
    }
}
