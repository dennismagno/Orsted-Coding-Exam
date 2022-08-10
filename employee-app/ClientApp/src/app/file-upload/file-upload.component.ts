import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  @ViewChild("fileData", {static: false}) fileData: ElementRef | undefined;
  constructor() { }
  
  @Output() newFileUpload : EventEmitter<any> = new EventEmitter<any>();

  ngOnInit(): void {
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    
    // Clear the input
    event.target.value = null;

    if (file) {
      this.newFileUpload.emit(file);
    }
  }
}
