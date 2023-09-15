import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NewestStoriesComponent } from './newest-stories.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { HackersService } from '../../Services/hackers.service';
import { Observable, of } from 'rxjs';

describe('NewestStoriesComponent', () => {

  let component: NewestStoriesComponent;
  let fixture: ComponentFixture<NewestStoriesComponent>;
  let hackerService: HackersService;
 
  beforeEach(() => {
    
    TestBed.configureTestingModule({
      providers: [
        HackersService  
      ],
      imports: [
        HttpClientTestingModule,
        NgxPaginationModule,
        FormsModule
      ],
      declarations: [
        NewestStoriesComponent,
      ],
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA,
      ]
    });
    fixture = TestBed.createComponent(NewestStoriesComponent);
    component = fixture.componentInstance;
    hackerService = TestBed.inject(HackersService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('getTopStories', () => {

    var spyGetTopStories: jasmine.Spy;

    beforeEach(() => {
      spyGetTopStories = spyOn(hackerService, 'getTopStories').and.callThrough();
    });

    it('should call getTopStories method of hackerService and set isLoading false', () => {
      component.getTopStories('');
      expect(hackerService.getTopStories).toHaveBeenCalled();
      expect(component.isLoading).toBeTruthy();
    });

    it('isLoading should be false after api calling subscribed', async () => {
      spyGetTopStories.and.returnValue(of([]));
      await component.getTopStories('');
      expect(component.isLoading).toBeFalsy();
    });

  });

  describe(('onPageSizeChange'), () => {

    it('should set pagingSize and page of pagingOptions', () => {
      var event = {
        target: {
          value: 10,
        }
      }
      component.onPageSizeChange(event);
      expect(component.pagingOptions.pagingSize).toEqual(10);
      expect(component.pagingOptions.page).toEqual(1);
    });

  });

  describe(('onSearch'), () => {

    it('should call getTopStories with search input value', () => {
      spyOn(component, 'getTopStories');
      component.searchValue = 'test';
      component.onSearch();
      expect(component.getTopStories).toHaveBeenCalledWith('test');
      expect(component.pagingOptions.page).toEqual(1);
    });

  });

});
