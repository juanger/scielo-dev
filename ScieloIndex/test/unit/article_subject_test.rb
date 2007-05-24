require File.dirname(__FILE__) + '/../test_helper'

class ArticleSubjectTest < Test::Unit::TestCase
  fixtures :subjects, :articles, :article_subjects

  def setup
    @article_subjects = [:art1fisica, :art1geo, :art2fisica, :art3quimica, :art3bio]
    @myarticle_subject = {:article_id => 1, :subject_id => 3}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_article_subjects_from_fixtures
    @article_subjects.each { |article_subject|
      @article_subject = article_subjects(article_subject)
      @article_subject_db = ArticleSubject.find(@article_subject.id)
      assert_equal @article_subject.id, @article_subject_db.id
      assert_equal @article_subject.article_id, @article_subject_db.article_id
      assert_equal @article_subject.subject_id, @article_subject_db.subject_id
    }
  end

  def test_deleting
    @article_subjects.each { |article_subject|
      @article_subject = article_subjects(article_subject)
      @article_subject_db = ArticleSubject.find_by_id(@article_subject.id)
      assert @article_subject_db.destroy
      @article_subject_db = ArticleSubject.find_by_id(@article_subject.id)
      assert @article_subject_db.nil?
    }
  end

  def test_creating_empty_article_subjecte
    @article_subject = ArticleSubject.new()
    assert !@article_subject.save
  end

  def test_uniqueness
    @article_subject = ArticleSubject.new(@myarticle_subject)
    assert @article_subject.save
    @article_subject.subject_id = subjects(:fisica).id
    assert !@article_subject.save
  end

  # Boundary
  def test_bad_values_for_id
    @article_subject = ArticleSubject.new(@myarticle_subject)

    # Checking for id constraints
    @article_subject.id = nil
    assert @article_subject.valid?

    @article_subject.id = -2
    assert !@article_subject.valid?

    @article_subject.id = 5.6
    assert !@article_subject.valid?
  end

  def test_bad_values_for_article_id
    @article_subject = ArticleSubject.new(@myarticle_subject)

    # Checking for article_id constraints
    @article_subject.article_id = nil
    assert !@article_subject.valid?

    @article_subject.article_id = -2
    assert !@article_subject.valid?

    @article_subject.article_id = 5.6
    assert !@article_subject.valid?
  end

  def test_bad_values_for_subject_id
    @article_subject = ArticleSubject.new(@myarticle_subject)

    # Checking for subject_id constraints
    @article_subject.subject_id = nil
    assert !@article_subject.valid?

    @article_subject.subject_id = -2
    assert !@article_subject.valid?

    @article_subject.subject_id = 5.6
    assert !@article_subject.valid?
  end

  def test_belongs_to_article
    @myarticle_subject = ArticleSubject.new(@myarticle_subject)
    assert_equal articles(:article1).id , @myarticle_subject.article.id
    assert_equal articles(:article1).title , @myarticle_subject.article.title
    assert_equal articles(:article1).journal_issue_id , @myarticle_subject.article.journal_issue_id
    assert_equal articles(:article1).fpage , @myarticle_subject.article.fpage
    assert_equal articles(:article1).lpage , @myarticle_subject.article.lpage
    assert_equal articles(:article1).page_range , @myarticle_subject.article.page_range
    assert_equal articles(:article1).url , @myarticle_subject.article.url
    assert_equal articles(:article1).pacsnum , @myarticle_subject.article.pacsnum
    assert_equal articles(:article1).other , @myarticle_subject.article.other
  end

  def test_belongs_to_subject
    @myarticle_subject = ArticleSubject.new(@myarticle_subject)
    assert_equal subjects(:atm).id , @myarticle_subject.subject.id
    assert_equal subjects(:atm).name , @myarticle_subject.subject.name
  end
end
