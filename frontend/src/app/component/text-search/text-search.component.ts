import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { OpenSearchService } from 'src/app/services/open-search.service';

@Component({
  selector: 'app-text-search',
  templateUrl: './text-search.component.html',
  styleUrls: ['./text-search.component.css']
})
export class TextSearchComponent implements OnInit {

  textSearchformGroup!: FormGroup;

  searchResults: any;

  constructor(private fb: FormBuilder, private service: OpenSearchService) { }

  ngOnInit(): void {

    this.textSearchformGroup = this.fb.group({

      searchWord: new FormControl('', Validators.compose([
        Validators.required
      ])),

      market: new FormControl([], Validators.compose([
      ])),
    })

  }

  search(formValue: any) {

    let markets = null;
    if (formValue.market.length > 0) {
      markets = formValue.market.split(",")
    }

    this.service.search(formValue.searchWord, markets).subscribe(response => {

      this.searchResults =  JSON.stringify(response, null, 2);

    });
  }

}
