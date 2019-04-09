import { Component, OnInit } from '@angular/core';
import { IdeService } from '../ide.service';
import { Router, ActivatedRoute} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

declare var $: any;
declare var ace: any;

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  public data;
  public search;
  public p = 1;
  constructor(private ideService: IdeService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {


    this.ideService.getHistory().subscribe(data => this.data = data);
    $(document).ready( () => {
      $('.btn-filter').click( () => {
        if ( $('#search').css('display') === 'block' ) {
          $('#search').css('display', 'none');
        } else {
          $('#search').css('display', 'block');
        }
      });
  });

  }

  onView(data) {
    this.router.navigate(['/view', data.CodeID]);
  }
  onEdit(data) {
    this.router.navigate(['/edit', data.CodeID]);
  }
  onDelete(data) {
    this.ideService.deleteFile({ CodeID : data.CodeID}).subscribe(status => {
      if (status.statusCode === '1') {
        this.toastr.success('File Deleted Successfuly');
        this.router.navigate(['/history']);
      } else {
        this.toastr.error('Something Went Wrong');
      }
     });
  }

}
