import { Subject } from 'rxjs';  
import { takeUntil } from 'rxjs/operators'; 
import { catchError, map } from 'rxjs/operators';  
import { Component, OnDestroy } from '@angular/core';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { AppService } from '../app.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnDestroy  {  
  constructor(public appService: AppService ){}
  employees: any = [];
  errorMsg: string = "";
  destroy: Subject<boolean> = new Subject<boolean>();
  
  ngOnDestroy() {
    this.destroy.next(true);`1`
    this.destroy.unsubscribe();
  }
  
  uploadFile(file:any) {  
    const formData = new FormData();  
    formData.append('file', file);  

    // Reset data
    this.employees = [];
    this.errorMsg = "";

    this.appService.uploadFile(formData)
      .pipe(takeUntil(this.destroy))
      .subscribe({
        next: (resp) => this.employees = resp.body,
        error: (err) => this.errorMsg = err.error
      });
  }
}
