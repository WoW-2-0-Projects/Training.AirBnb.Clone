/*
 * Represents a menu item
 */
export class MenuAction {
    public id: string;
    public title: string;
    public routeName?: string;
    public callback?: string;
    public special: boolean;
}