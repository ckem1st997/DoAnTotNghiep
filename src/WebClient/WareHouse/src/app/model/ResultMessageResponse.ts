
export interface ResultMessageResponse<T> {
    success: boolean;
    code: string;
    httpStatusCode: number;
    title: string;
    message: string;
    data: T[];
    totalCount: number;
    isRedirect: boolean;
    redirectUrl: string;
    errors: { [key: string]: string[]; };
}