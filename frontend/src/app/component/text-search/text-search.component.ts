import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { TextSearchService } from 'src/app/services/text-search.service';

@Component({
  selector: 'app-text-search',
  templateUrl: './text-search.component.html',
  styleUrls: ['./text-search.component.css']
})
export class TextSearchComponent implements OnInit {

  
  textSearchformGroup!: FormGroup;

  constructor(private fb: FormBuilder, private textSearchService: TextSearchService) { }

  ngOnInit(): void {

    this.textSearchformGroup = this.fb.group({

      searchWord: new FormControl('', Validators.compose([
        Validators.required
      ])),

      notepadText: new FormControl('', Validators.compose([
        Validators.required
      ])),
    })

  }

  searchWordInNotepad(formValue: any) {

  }

}
