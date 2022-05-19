export interface BaseSearchModel {
    keySearch: string;
    active: boolean | null;
    skip: number;
    take: number;
}