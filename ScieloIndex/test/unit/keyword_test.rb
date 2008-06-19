require File.dirname(__FILE__) + '/../test_helper'

class KeywordTest < Test::Unit::TestCase
  fixtures :articles, :keywords

  def setup
    @keywords = [:quakes, :storms, :eruptions, :floods]
    @mykeyword = {:name => 'tornados'}
  end

  #RIGHT CRUD (Create, Update and Delete)
  def test_creating_keywords_from_fixtures
    @keywords.each { |keyword|
      @keyword = keywords(keyword)
      @keyword_db = Keyword.find_by_name(@keyword.name)
      assert_equal @keyword.id, @keyword_db.id
      assert_equal @keyword.name, @keyword_db.name
    }
  end

  def test_updating
    @keywords.each { |keyword|
      @keyword = keywords(keyword)
      @keyword_db = Keyword.find_by_name(@keyword.name)
      @keyword_db.id = @keyword_db.id
      assert @keyword_db.save
      @keyword_db.name.reverse
      assert @keyword_db.save
    }
  end

  def test_deleting
    @keywords.each { |keyword|
      @keyword = keywords(keyword)
      @keyword_db = Keyword.find_by_name(@keyword.name)
      assert @keyword_db.destroy
      @keyword_db = Keyword.find_by_name(@keyword.name)
      assert @keyword_db.nil?
    }
    @collection = Keyword.find(:all)
    assert_equal 0, @collection.size
  end

  def test_creating_empty_keyword
    @keyword = Keyword.new()
    assert !@keyword.save
  end

  def test_checking_uniqueness
    @keyword = Keyword.new(@mykeyword)
    @keyword.name = 'massive floods'
    assert !@keyword.save
  end

  # Boundary
  def test_bad_values_for_id
    @keyword = Keyword.new(@mykeyword)

    # Checking for id constraints
    @keyword.id = nil
    assert @keyword.valid?

    @keyword.id = 5.2
    assert !@keyword.valid?

    @keyword.id = -10
    assert !@keyword.valid?
  end

  def test_bad_values_for_name
    @keyword = Keyword.new(@mykeyword)

    # Checking for name constraints
    @keyword.name = nil
    assert !@keyword.valid?

    @keyword.name = "AA"
    assert !@keyword.valid?

    @keyword.name = "A"*501
    assert !@keyword.valid?
  end

  def test_has_many_articles
    @mykeyword = Keyword.create(@mykeyword)
    @article_keyword = ArticleKeyword.create(:article_id => 1, :keyword_id => @mykeyword.id)
    assert_equal articles(:article1).id, @mykeyword.articles.first.id
    assert_equal articles(:article1).title, @mykeyword.articles.first.title
    assert_equal articles(:article1).fpage, @mykeyword.articles.first.fpage
    assert_equal articles(:article1).lpage, @mykeyword.articles.first.lpage
  end
end
