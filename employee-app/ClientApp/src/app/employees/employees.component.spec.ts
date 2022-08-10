import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesComponent } from './employees.component';

describe('EmployeesComponent', () => {
  let component: EmployeesComponent;
  let fixture: ComponentFixture<EmployeesComponent>;
  let  testData = [{ Number: "009", FirstName: "Dennis", LastName: "Magno", Status: "Regular"}, { Number: "001", FirstName: "Akiyho Dennis", LastName: "Magno", Status: "Contractor"}];
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render the table properly ', () => {
    component.employees = testData;
    fixture.detectChanges();
  
    let tableRows = fixture.nativeElement.querySelectorAll('tr');
    expect(tableRows.length).toBe(3);

    // Header row
    let headerRow = tableRows[0];
    expect(headerRow.cells[0].innerHTML).toBe('Employee Number');
    expect(headerRow.cells[1].innerHTML).toBe('First Name');
    expect(headerRow.cells[2].innerHTML).toBe('Last Name');
    expect(headerRow.cells[3].innerHTML).toBe('Employee Status');

    // Data rows
    let row1 = tableRows[1];
    expect(row1.cells[0].innerHTML).toBe('009');
    expect(row1.cells[1].innerHTML).toBe('Dennis');
    expect(row1.cells[2].innerHTML).toBe('Magno');
    expect(row1.cells[3].innerHTML).toBe('Regular');
    expect(row1.getAttribute('class')).toMatch('regular-font');
  });

  it('should render the row with proper class ', () => {
    component.employees = testData;
    fixture.detectChanges();

    let tableRows = fixture.nativeElement.querySelectorAll('tr');
    expect(tableRows.length).toBe(3)

    let row1 = tableRows[1];
    expect(row1.getAttribute('class')).toMatch('regular-font');

    let row2 = tableRows[2];
    expect(row2.getAttribute('class')).toMatch('contractor-font');
  });
});
