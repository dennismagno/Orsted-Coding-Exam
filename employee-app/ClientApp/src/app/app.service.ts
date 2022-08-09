import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http: HttpClient) { }

  rootURL = '/api';

  uploadFile(formData: any) {
    return this.http.post<any>(this.rootURL + '/FileUpload/', formData, { observe: 'response' });  
  }
}