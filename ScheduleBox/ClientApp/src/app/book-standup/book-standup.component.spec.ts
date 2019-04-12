import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookStandupComponent } from './book-standup.component';

describe('BookStandupComponent', () => {
  let component: BookStandupComponent;
  let fixture: ComponentFixture<BookStandupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookStandupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookStandupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
