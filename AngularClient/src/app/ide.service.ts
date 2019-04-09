import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IdeService {

  constructor(private http: HttpClient) { }

  compileCode(data) {
    return this.http.post<any>(environment.apiEndPoint + '/compilecode' , data);
  }
  getHistory() {
    return this.http.get<any>(environment.apiEndPoint + '/getHistory');
  }
  getFile(data) {
    return this.http.post<any>(environment.apiEndPoint + '/getFile', data);
  }
  updateFile(data) {
    return this.http.post<any>(environment.apiEndPoint + '/updateFile', data);
  }
  deleteFile(data) {
    return this.http.post<any>(environment.apiEndPoint + '/deleteFile', data);
  }
}
