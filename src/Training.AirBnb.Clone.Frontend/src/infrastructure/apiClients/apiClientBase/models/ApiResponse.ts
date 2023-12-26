import type { ProblemDetails } from "./ProblemDetails";

export class ApiResponse<T> {
    public response: T | null;
    public error: ProblemDetails | null;
    public status: number;

    constructor(response: T | null, error: ProblemDetails | null, status: number) {
        this.response = response;
        this.error = error;
        this.status = status;
    }

    public get isSuccess(): boolean {
        return this.status >= 200 && this.status < 300;
    }
}