export class DocumentService {

    public getRemainingScrollOnLeft(element: HTMLElement): number {
        return element.scrollWidth - element.scrollLeft - element.clientWidth;
    }

    public getRemainingScrollOnRight(element: HTMLElement): number {
        return element.scrollWidth - (element.scrollLeft + element.clientWidth);
    }

    public scrollLeft(element: HTMLElement, distance: number) {
        const remainingScrollOnLeft = this.getRemainingScrollOnLeft(element);
        const fullScroll = remainingScrollOnLeft < distance * 2;
        const scrollDistance = fullScroll ? element.scrollLeft - element.clientWidth : element.scrollLeft - distance;

        element.scroll({
            left: scrollDistance,
            behavior: "smooth"
        });
    }

    public scrollRight(element: HTMLElement, distance: number) {
        const remainingScrollOnRight = this.getRemainingScrollOnRight(element);
        const fullScroll = remainingScrollOnRight < distance * 2;
        const scrollDistance = fullScroll ? element.scrollWidth - element.clientWidth : element.scrollLeft + distance;

        element.scroll({
            left: scrollDistance,
            behavior: "smooth"
        });
    }

    public canScrollLeft(element: HTMLElement): boolean {
        return element.scrollLeft > 0;
    }

    public canScrollRight(element: HTMLElement, minDistance: number = 0): boolean {
        return element.scrollLeft + element.clientWidth < element.scrollWidth - minDistance;
    }

    public addEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.addEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }

    public removeEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.removeEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }
}