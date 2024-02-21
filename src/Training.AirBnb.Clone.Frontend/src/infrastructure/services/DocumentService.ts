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

    public scrollTo(element: HTMLElement, position: number) {
        element.scrollTo({
            left: position,
            behavior: "smooth"
        });
    }

    public scrollToBeginning(element: HTMLElement) {
        element.scroll({
            left: 0,
            behavior: "smooth"
        });
    }

    public scrollToEnd(element: HTMLElement) {
        element.scroll({
            left: element.scrollWidth,
            behavior: "smooth"
        });
    }
    
    public getChildWidth(element: HTMLElement): number {
        return element.children.length == 0 ? 0 : (element.children[0] as HTMLElement).offsetWidth;
    }

    public addEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.addEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }

    public removeEventListener(element: HTMLElement, eventName: string, callback: (event: HTMLElement) => void): void {
        element.removeEventListener(eventName, (event: Event) => callback(event.target as HTMLElement));
    }

    public addWindowEventListener(eventName: string, callback: (event: Event) => void): void {
        window.addEventListener(eventName, callback);
    }

    public removeWindowEventListener(eventName: string, callback: (event: Event) => void): void {
        window.removeEventListener(eventName, callback);
    }

    public handleBodyOverflow(isModalActive: boolean): void {
        if (isModalActive) {
            document.body.classList.add("overflow-hidden");
        } else {
            document.body.classList.remove("overflow-hidden");
        }
    }

    public getHeight(element: HTMLElement): number {
        return element.offsetHeight;
    }

    public isDocumentScrolledToBottom(minimumSpace: number = 0) {
        const scrollPosition = window.innerHeight + window.scrollY;
        const threshold = document.body.scrollHeight - (minimumSpace || 0);
        return scrollPosition >= threshold;
    }
}