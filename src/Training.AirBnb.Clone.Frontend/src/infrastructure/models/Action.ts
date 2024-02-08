/*
 * Represents an abstract callback
 */
export class Action {

    constructor(callback: () => void) {
        this.callBack = callback;
    }

    public callBack: () => void;
}

/*
 * Represents notification source
 */
export class NotificationSource {

    private listeners: Action[] = [];

    public updateListeners = () => {
        this.listeners.forEach((listener: Action) => {

            listener.callBack();
        });
    }

    public addListener = (callback: () => void) => {
        this.listeners.push(new Action(callback));
    }
}