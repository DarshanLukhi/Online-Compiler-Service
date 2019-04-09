import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { IdeService } from '../ide.service';
declare var $: any;
declare var ace: any;

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  public data;
  public id;
  public editor;
  public theme = 'ace/theme/clouds';
  constructor(private ideService: IdeService, private route: ActivatedRoute) { }

  ngOnInit() {
    const that = this;
    let content;
    that.editor = ace.edit('editor');
    that.editor.setTheme('ace/theme/clouds');
    this.id = this.route.snapshot.paramMap.get('CodeID');
    let x;

    x = { CodeID : this.id};
    this.ideService.getFile(x).subscribe(data => {
      console.log(data);
      if (data.error === 'Not Found') {
        content = 'Invalid Code ID';
      } else {
        content = data.Code;
      }
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
        readOnly: true
      });
      that.editor.session.setValue(content);
    });

  }
  onThemeChange() {
    this.editor.setTheme(this.theme);
  }

}
