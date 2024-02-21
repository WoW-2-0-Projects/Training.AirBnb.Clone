/*
 * Represents a menu item
 */
export class MenuItem {
    public id!: string;
    public title!: string;
    public routeName?: string;
    public callback?: () => void;
    public special: boolean;

    constructor() {
        this.special = false;
    }
}