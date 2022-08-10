// app.component.spec.ts
import { TestBed, async } from '@angular/core/testing'; // 1
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { EmployeesComponent } from './employees/employees.component';
import { HomeComponent } from './home/home.component';
import { RouterTestingModule } from '@angular/router/testing';
describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        FileUploadComponent,
        EmployeesComponent,
        NavMenuComponent,
        HomeComponent
      ],
      imports: [ RouterTestingModule ]
    }).compileComponents();
  }));
 
  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
 
  it(`should have as title 'Angular + .Net Core Employee App'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Angular + .Net Core Employee App');
  });
 
  it('should render title in a navbar tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('.container .navbar-brand').textContent).toContain('Angular + .Net Core Employee App');
  });
});