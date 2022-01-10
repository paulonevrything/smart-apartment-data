import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OpenSearchService {

  searchUrl: string = environment.openSearchUrl;

  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin':'*'
  })

  constructor(private http: HttpClient) { }


  search(searchPhrase: string, markets: string[]): Observable<any> {

    let params = new HttpParams();
    params = params.append("searchPhrase", searchPhrase.trim())
    
    if (markets) {
      markets.forEach(market => {
        params = params.append("markets", market.trim())
      });
    }

    return this.http.get(this.searchUrl, {
      params: params,
      headers: this.headers
    });
  }
}
