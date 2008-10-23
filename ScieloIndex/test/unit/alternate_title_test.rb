require File.dirname(__FILE__) + '/../test_helper'

class AlternateTitleTest < Test::Unit::TestCase
  fixtures :languages, :journals, :journal_issues, :articles, :alternate_titles

  def setup
    @alternate_titles = [:alternate1, :alternate2, :alternate3]
    @myalternate_title = { :title => 'Implications of the Global Warming in the precipitation in the North Zone of Mexico', :language_id => 39, :article_id => 4}
  end

  # RIGHT
  def test_creating_alternate_title_from_fixtures
    @alternate_titles.each { |alternate_title|
      @alternate_title = alternate_titles(alternate_title)
      @alternate_title_db = AlternateTitle.find_by_title(@alternate_title.title)
      assert_equal @alternate_title.id, @alternate_title_db.id
      assert_equal @alternate_title.title, @alternate_title_db.title
      assert_equal @alternate_title.language_id, @alternate_title_db.language_id
      assert_equal @alternate_title.article_id, @alternate_title_db.article_id
    }
  end

  def test_updating
    @alternate_titles.each { |alternate_title|
      @alternate_title = alternate_titles(alternate_title)
      @alternate_title_db = AlternateTitle.find_by_title(@alternate_title.title)
      @alternate_title_db.title.reverse
      assert @alternate_title_db.save
    }
   end

  def test_deleting
    @alternate_titles.each { |alternate_title|
      @alternate_title = alternate_titles(alternate_title)
      @alternate_title_db = AlternateTitle.find_by_title(@alternate_title.title)

      assert_equal @alternate_title.id, @alternate_title_db.id
      assert @alternate_title_db.destroy
      @alternate_title_db = AlternateTitle.find_by_title(@alternate_title.title)
      assert @alternate_title_db.nil?
    }
  end

  def test_creating_empty_alternate_title
    @alternate_title = AlternateTitle.new()
    assert !@alternate_title.save
  end

  def test_checking_uniqueness_title
    @alternate_title = AlternateTitle.new(@myalternate_title)
    @alternate_title.id = 4
    @alternate_title.title  = 'Calefacción inducida relámpago del ionosfera'
    @alternate_title.language_id = 41
    @alternate_title.article_id = 2
    assert !@alternate_title.save
  end

  # Boundary
  def test_bad_values_for_id
    @alternate_title = AlternateTitle.new(@myalternate_title)

    # Checking for ID constraints
    @alternate_title.id = nil
    assert @alternate_title.valid?

    @alternate_title.id = -2
    assert !@alternate_title.valid?

    @alternate_title.id = 5.6
    assert !@alternate_title.valid?
  end

  def test_bad_values_for_title
    @article = Article.create(@myarticle)

    # Checking title constraints
    @article.title = nil
    assert !@article.valid?
    
    @article.title = ""
    assert !@article.valid?
    
    @article.title = "A"*100000
    assert !@article.valid?
  end

  def test_bad_values_for_language_id
    @alternate_title = AlternateTitle.new(@myalternate_title)

    # Checking for LANGUAGE_ID constraints
    @alternate_title.language_id = nil
    assert !@alternate_title.valid?

    @alternate_title.language_id = -2
    assert !@alternate_title.valid?

    @alternate_title.language_id = 5.6
    assert !@alternate_title.valid?
  end

  def test_bad_values_for_article_id
    @alternate_title = AlternateTitle.new(@myalternate_title)

    # Checking for ARTICLE_ID constraints
    @alternate_title.article_id = nil
    assert !@alternate_title.valid?

    @alternate_title.article_id = -2
    assert !@alternate_title.valid?

    @alternate_title.article_id = 5.6
    assert !@alternate_title.valid?
  end
end
