export class DocumentService {

    public getRemainingScrollOnLeft(element: HTMLElement): number {
        return element.scrollLeft;
    }

    public getRemainingScrollOnRight(element: HTMLElement): number {
        return element.scrollWidth - element.clientWidth - element.scrollLeft;
    }    

    public canScrollLeft(element: HTMLElement, minDistance: number = 0): boolean {
        return element.scrollLeft > minDistance;
    }

    public canScrollRight(element: HTMLElement, minDistance: number = 0): boolean {
        return this.getRemainingScrollOnRight(element) > minDistance;
    }

    public scrollLeft(element: HTMLElement) {
        const remainingScrollOnLeft = this.getRemainingScrollOnLeft(element);
        const distance = (2 * element.clientWidth) / 3;

        if (remainingScrollOnLeft < element.clientWidth) {
            this.scrollToBeginning(element);
        }
        else {
            element.scrollBy({
                left: -distance,
                behavior: "smooth"
            });
        }
    }

    public scrollRight(element: HTMLElement) {
        const remainingScrollOnRight = this.getRemainingScrollOnRight(element);
        const distance = (2 * element.clientWidth) / 3;
        
        if (remainingScrollOnRight < element.clientWidth) {
            this.scrollToEnd(element);
        }
        else {
            element.scrollBy({
                left: distance,
                behavior: "smooth"
            });
        }
    }

    public addEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.addEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }

    public removeEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.removeEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }

    public handleBodyOverflow(isModalActive: boolean): void {
        if (isModalActive) {
            document.body.classList.add("overflow-hidden");
        } else {
            document.body.classList.remove("overflow-hidden");
        }
    }
}