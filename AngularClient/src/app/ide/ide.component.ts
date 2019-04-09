import { Component, OnInit } from '@angular/core';
import { IdeService } from './../ide.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute} from '@angular/router';
declare var $: any;
declare var ace: any;


@Component({
  selector: 'app-ide',
  templateUrl: './ide.component.html',
  styleUrls: ['./ide.component.css']
})
export class IdeComponent implements OnInit {
  public link;
  code = '';
  input = '';
  inputRadio = true;
  lang = 'C';
  output;
  error;
  status;
  id;
  public editor;
  public theme = 'ace/theme/clouds';
  public buf;


  constructor(private ideService: IdeService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) { }


  ngOnInit() {

    const that = this;
    $(document).ready(() => {
      $('#toggle').click(() => {
        $('#input').slideToggle();
      });
      ace.require('ace/ext/language_tools');
      that.editor = ace.edit('editor');
      that.editor.setTheme('ace/theme/clouds');
      that.editor.session.setMode('ace/mode/c_cpp');
      that.editor.setOptions({
        enableBasicAutocompletion: true,
        enableSnippets: true,
        enableLiveAutocompletion: true
    });
    });
  }


  onLanguageChange() {
    if (this.lang === 'Python') {
      this.editor.session.setMode('ace/mode/python');
    }
    if (this.lang === 'Java') {
      this.editor.session.setMode('ace/mode/java');
    }
    if (this.lang === 'C' || this.lang === 'C++') {
      this.editor.session.setMode('ace/mode/c_cpp');
    }
  }
  onThemeChange() {
    this.editor.setTheme(this.theme);
  }
  onRunCode() {
    this.code = this.editor.getValue();
    const data = {
      code: this.code,
      input: this.input,
      inputRadio: this.inputRadio,
      lang: this.lang
    };
    this.ideService.compileCode(data).subscribe(
      status => {
        this.id = status.id;
        this.status = status.status;
        this.output = status.output;
        this.error = status.error;
        this.link = 'http://localhost:4200/view/' + this.id;
        if (status.status === 'AC') {
          this.toastr.success( 'Compiled Successfully', 'AC');
        } else if (status.status === 'CTE') {
          this.toastr.error('Compile Time Error', 'CTE');
        } else if (status.status === 'RTE') {
          this.toastr.error( 'Run Time Error', 'RTE');
        } else if (status.status === 'TLE') {
          this.toastr.warning( 'Time Limit Exceeded', 'TLE');
        }

      }
    );
  }
}
