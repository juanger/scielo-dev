require File.dirname(__FILE__) + '/../test_helper'

class ArticleKeywordTest < Test::Unit::TestCase
  fixtures :keywords, :articles, :article_keywords

  def setup
    @article_keywords = [:art1quakes, :art2storms, :art3eruptions, :art3floods]
    @myarticle_keyword = {:article_id => 1, :keyword_id => 2}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_article_keywords_from_fixtures
    @article_keywords.each { |article_keyword|
      @article_keyword = article_keywords(article_keyword)
      @article_keyword_db = ArticleKeyword.find(@article_keyword.id)
      assert_equal @article_keyword.id, @article_keyword_db.id
      assert_equal @article_keyword.article_id, @article_keyword_db.article_id
      assert_equal @article_keyword.keyword_id, @article_keyword_db.keyword_id
    }
  end

  def test_updating
      @article_keywords.each { |article_keyword|
      @article_keyword = article_keywords(article_keyword)
      @article_keyword_db = ArticleKeyword.find(@article_keyword.id)
      @article_keyword_db.id = @article_keyword_db.id
      assert @article_keyword_db.update
      @article_keyword_db.article_id = @article_keyword_db.article_id
      assert @article_keyword_db.update
      @article_keyword_db.keyword_id = @article_keyword_db.keyword_id
      assert @article_keyword_db.update
    }
  end

  def test_deleting
    @article_keywords.each { |article_keyword|
      @article_keyword = article_keywords(article_keyword)
      @article_keyword_db = ArticleKeyword.find_by_id(@article_keyword.id)
      assert @article_keyword_db.destroy
      @article_keyword_db = ArticleKeyword.find_by_id(@article_keyword.id)
      assert @article_keyword_db.nil?
    }
  end

  def test_creating_empty_article_keyword
    @article_keyword = ArticleKeyword.new()
    assert !@article_keyword.save
  end

  def test_uniqueness
    @article_keyword = ArticleKeyword.new(@myarticle_keyword)
    assert @article_keyword.save
    @article_keyword.keyword_id = keywords(:quakes).id
    assert !@article_keyword.save
  end

  # Boundary
  def test_bad_values_for_id
    @article_keyword = ArticleKeyword.new(@myarticle_keyword)

    # Checking for id constraints
    @article_keyword.id = nil
    assert @article_keyword.valid?

    @article_keyword.id = -2
    assert !@article_keyword.valid?

    @article_keyword.id = 5.6
    assert !@article_keyword.valid?
  end

  def test_bad_values_for_article_id
    @article_keyword = ArticleKeyword.new(@myarticle_keyword)

    # Checking for article_id constraints
    @article_keyword.article_id = nil
    assert !@article_keyword.valid?

    @article_keyword.article_id = -2
    assert !@article_keyword.valid?

    @article_keyword.article_id = 5.6
    assert !@article_keyword.valid?
  end

  def test_bad_values_for_keyword_id
    @article_keyword = ArticleKeyword.new(@myarticle_keyword)

    # Checking for keyword_id constraints
    @article_keyword.keyword_id = nil
    assert !@article_keyword.valid?

    @article_keyword.keyword_id = -2
    assert !@article_keyword.valid?

    @article_keyword.keyword_id = 5.6
    assert !@article_keyword.valid?
  end

  def test_belongs_to_article
    @myarticle_keyword = ArticleKeyword.new(@myarticle_keyword)
    assert_equal articles(:article1).id , @myarticle_keyword.article.id
    assert_equal articles(:article1).title , @myarticle_keyword.article.title
    assert_equal articles(:article1).journal_issue_id , @myarticle_keyword.article.journal_issue_id
    assert_equal articles(:article1).fpage , @myarticle_keyword.article.fpage
    assert_equal articles(:article1).lpage , @myarticle_keyword.article.lpage
    assert_equal articles(:article1).page_range , @myarticle_keyword.article.page_range
    assert_equal articles(:article1).url , @myarticle_keyword.article.url
    assert_equal articles(:article1).pacsnum , @myarticle_keyword.article.pacsnum
    assert_equal articles(:article1).other , @myarticle_keyword.article.other
  end

    def test_belongs_to_keyword
    @myarticle_keyword = ArticleKeyword.new(@myarticle_keyword)
    assert_equal keywords(:storms).id , @myarticle_keyword.keyword.id
    assert_equal keywords(:storms).name , @myarticle_keyword.keyword.name
  end
end
