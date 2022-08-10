import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {By} from "@angular/platform-browser";
import { of } from 'rxjs';

import { HomeComponent } from './home.component';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { FileUploadComponent } from '../file-upload/file-upload.component';
import { EmployeesComponent } from '../employees/employees.component';

import { AppService } from '../app.service';
import { AppServiceStub } from '../app.service.stub';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('AppService', ['uploadFile']);
    await TestBed.configureTestingModule({
        imports: [HttpClientTestingModule], 
        declarations: [ HomeComponent, NavMenuComponent, EmployeesComponent, FileUploadComponent ],
        providers: [{ provide: AppService, useClass: AppServiceStub }],
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should create file upload component', () => {
    const fileInput  = fixture.nativeElement.querySelectorAll('input#fileData');
    expect(fileInput).toBeTruthy();
  });

  it('should create employees component', () => {
    let tableRows = fixture.nativeElement.querySelectorAll('tr');
    expect(tableRows.length).toBe(1);
  });
});
