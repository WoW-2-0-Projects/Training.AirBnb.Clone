export class FilterPagination {
    public pageSize: number;
    public pageToken: number;

    constructor(pageSize: number, pageToken: number){
        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}