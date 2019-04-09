import { Component, OnInit } from '@angular/core';
import { IdeService } from '../ide.service';
import { Router, ActivatedRoute} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

declare var $: any;
declare var ace: any;


@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {

  public data;
  public code;
  public id;
  public editor;
  public theme = 'ace/theme/clouds';
  constructor(private ideService: IdeService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    const that = this;
    that.editor = ace.edit('editor');
    that.editor.setTheme('ace/theme/clouds');
    this.id = this.route.snapshot.paramMap.get('CodeID');
    let x;
    x = { CodeID : this.id};
    this.ideService.getFile(x).subscribe(data => {

      if (data.Language === 'Python') {
        this.editor.session.setMode('ace/mode/python');
      }
      if (data.Language === 'Java') {
        this.editor.session.setMode('ace/mode/java');
      }
      if (data.Language === 'C' || data.Language === 'C++') {
        this.editor.session.setMode('ace/mode/c_cpp');
      }
      that.editor.setOptions({
        readOnly: false
      });
      that.editor.session.setValue(data.Code);
    });

  }
  onThemeChange() {
    this.editor.setTheme(this.theme);
  }
  onUpdateCode() {
    this.code = this.editor.getValue();
    const data = {
      code: this.code,
      CodeID: this.id
    };
    this.ideService.updateFile(data).subscribe(
      status => {
        if (status.statusCode === '1') {
          this.toastr.success('File Updated Successfuly');
          this.router.navigate(['/view', data.CodeID]);
        } else {
          this.toastr.error('File Does Not Exist');
        }
      }
    );
  }


}
