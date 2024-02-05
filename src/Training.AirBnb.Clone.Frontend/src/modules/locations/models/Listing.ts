import type { Address } from "./Address";
import type { Money } from "./Money";
import type { Rating } from "./Rating";

export class Listing {
    public id!: string;
    public name!: string;
    public builtDate!: Date;
    public address!: Address;
    public pricePerNight!: Money;
    public rating!: Rating;
    public imagesUrls!: Array<string>;
}