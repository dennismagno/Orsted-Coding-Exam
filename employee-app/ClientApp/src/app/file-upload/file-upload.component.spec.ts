import { ComponentFixture, TestBed } from '@angular/core/testing';
import {By} from "@angular/platform-browser";

import { FileUploadComponent } from './file-upload.component';

describe('FileUploadComponent', () => {
  let component: FileUploadComponent;
  let fixture: ComponentFixture<FileUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should detect file input change and trigger newFileUpload event', () => {
    const file = new File([''], 'test-file.xlsx');
    const dataTransfer = new DataTransfer();
    dataTransfer.items.add(new File([''], 'test-file.xlsx'))
    spyOn(component.newFileUpload, 'emit');

    const fileInput  = fixture.debugElement.query(By.css('input[type=file]'));
    fileInput.nativeElement.files = dataTransfer.files;

    fileInput.nativeElement.dispatchEvent(new Event('change'));
    fixture.detectChanges();

    expect(component.newFileUpload.emit).toHaveBeenCalledWith(file);
  });

  it('file change event should arrive in handler', () => {
    const input = fixture.debugElement.query(By.css('input[type=file]'));

    spyOn(component, 'onFileSelected');

    input.nativeElement.dispatchEvent(new Event('change'));
    fixture.detectChanges();

    expect(component.onFileSelected).toHaveBeenCalled();
  });
});
