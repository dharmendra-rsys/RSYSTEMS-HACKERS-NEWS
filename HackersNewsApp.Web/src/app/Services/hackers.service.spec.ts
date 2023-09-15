import { TestBed } from '@angular/core/testing';
import { HackersService } from './hackers.service';
import { HttpClientModule } from '@angular/common/http';
import { TOP_STORIES_URL } from '../app.constants';

describe('HackersService', () => {

  let hackerService: HackersService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
    });
    hackerService = TestBed.inject(HackersService);
  });

  it('should be created', () => {
    expect(hackerService).toBeTruthy();
  });

  it('getTopStories should call http get method', () => {
    spyOn(hackerService['http'], 'get');
    hackerService.getTopStories('test');
    expect(hackerService['http'].get).toHaveBeenCalledWith(`${TOP_STORIES_URL}?searchValue=test`);
  });

});
