import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { TOP_STORIES_URL } from '../app.constants';
import { HackerStory } from "../Interface/hackerStory.interface";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class HackersService {
  constructor(private http: HttpClient) {
  }

  getTopStories(searchValue: string): Observable<HackerStory[]>{
    return this.http
      .get<HackerStory[]>(
        `${TOP_STORIES_URL}?searchValue=${searchValue}`
      )
  }
}
