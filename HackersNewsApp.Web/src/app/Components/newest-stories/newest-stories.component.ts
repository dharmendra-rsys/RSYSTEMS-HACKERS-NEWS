import { Component, OnInit } from '@angular/core';
import { Paging } from '../../Models/paging.model';
import { HackersService } from '../../Services/hackers.service';
import { HackerStory } from '../../Interface/hackerStory.interface';

@Component({
  selector: 'app-newest-stories',
  templateUrl: './newest-stories.component.html',
  styleUrls: ['./newest-stories.component.css']
})
export class NewestStoriesComponent implements OnInit {
  isLoading: boolean = true;
  searchValue = '';
  pagingOptions = new Paging();
  topStories: HackerStory[] = [];

  constructor(private hackersService: HackersService) {
  }

  ngOnInit(): void {
    this.getTopStories(this.searchValue);
  }

  //
  getTopStories(searchValue: string) {
    this.isLoading = true;
    this.hackersService.getTopStories(searchValue).subscribe((stories) => {
      this.topStories = stories;
      this.pagingOptions.count = stories.length;
      this.isLoading = false;
    });
  }

  //
  onPageSizeChange(event: any) {
    this.pagingOptions.pagingSize = event.target.value;
    this.pagingOptions.page = 1;
  }

  //
  onSearch() {
    this.getTopStories(this.searchValue);
    this.pagingOptions.page = 1;
  }

}
