export interface Errors {
    Code: string[];
}

export interface ReponseValidator {
    errors: Errors;
    type: string;
    title: string;
    status: number;
    traceId: string;
}