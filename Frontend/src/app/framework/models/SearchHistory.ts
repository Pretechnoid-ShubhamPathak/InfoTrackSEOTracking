export class SearchHistory {
  id!:number;
  keywords!:string;
  url!:string;
  ranking!:string;
  searchEngine!:string;
  searchedAt!:string;
}

export class SearchHistoryRequest {
  keywords!:string;
  url!:string;
  searchEngine!:string;
}
