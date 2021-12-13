import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-view-files',
  templateUrl: './view-files.component.html',
  styleUrls: ['./view-files.component.css']
})
export class ViewFilesComponent implements OnInit {

  constructor(private http:  HttpClient) { }

  ngOnInit(): void {
    this.getAllFiles();
  }
  files: any = [];
  getAllFiles()
  {
    debugger
    return this.http.get('http://localhost:8083/Files')
    .subscribe((result) => {
      this.files = result;
      console.log(result);
  });
  }

  downloadFile(id: number, contentType: string)
  {
    return this.http.get(`http://localhost:8083/Files/${id}`, {responseType: 'blob'})
    .subscribe((result: Blob) => {
      const blob = new Blob([result], { type: contentType }); // you can change the type
      const url= window.URL.createObjectURL(blob);
      window.open(url);
      console.log("Success");
  });
  }

}
