/*
 * Represents an abstract callback
 */
export class Action {
    public callBack: () => void;
}

/*
 * Represents notification source
 */
export class NotificationSource extends Action {
}