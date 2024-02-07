/*
 * Represents a menu item
 */
export class MenuItem {
    public id: string;
    public title: string;
    public routeName?: string;
    public callback?: string;
    public special: boolean;
}