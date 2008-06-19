require File.dirname(__FILE__) + '/../test_helper'

class CiteTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues, :articles, :authors, :article_authors, :cites

  def setup
    @cites = [:cite1, :cite2, :cite3]
    @mycite = {:article_id => 1, :cited_by_article_id => 5, :cite_order => 3}
  end

  # RIGHT
  def test_creating_cites_from_fixtures
    @cites.each { |cite|
      @cite = cites(cite)
      @cite_db = Cite.find(@cite.id)
      assert_equal @cite.id, @cite_db.id
      assert_equal @cite.article_id, @cite_db.article_id
      assert_equal @cite.cited_by_article_id, @cite_db.cited_by_article_id
      assert_equal @cite.cite_order, @cite_db.cite_order
    }
  end

  def test_updating
    @cites.each { |cite|
      @cite = cites(cite)
      @cite_db = Cite.find(@cite.id)
      @cite_db.id = @cite_db.id
      assert @cite_db.save
      @cite_db.article_id = @cite_db.article_id
      assert @cite_db.save
      @cite_db.cited_by_article_id = @cite_db.cited_by_article_id
      assert @cite_db.save
      @cite_db.cite_order = @cite_db.cite_order
      assert @cite_db.save
    }
  end

  def test_deleting
    @cites.each { |cite|
      @cite = cites(cite)

      @cite_db = Cite.find_by_id(@cite.id)
      assert @cite_db.destroy
      @cite_db = Cite.find_by_id(@cite.id)
      assert @cite_db.nil?
    }
  end

  def test_creating_empty_cite
    @cite = Cite.new()
    assert !@cite.save
  end

  def test_checking_uniqueness
    @cite = Cite.new(@mycite)
    assert @cite.save
    @cite.cited_by_article_id = articles(:article2).id
    @cite = Cite.new(@mycite)
    assert !@cite.save
  end

  # Boundary
  def test_bad_values_for_id
    @cite = Cite.new(@mycite)

    # Checking for ID constraints
    @cite.id = nil
    assert @cite.valid?

    @cite.id = -2
    assert !@cite.valid?

    @cite.id = 5.6
    assert !@cite.valid?
  end

  def test_bad_values_for_article_id
    @cite = Cite.new(@mycite)

    # Checking for article_id constraints
    @cite.article_id = nil
    assert !@cite.valid?

    @cite.article_id = -2
    assert !@cite.valid?

    @cite.article_id = 5.6
    assert !@cite.valid?
  end

  def test_bad_values_for_cited_by_article_id
    @cite = Cite.new(@mycite)

    # Checking for cited_by_article_id constraints
    @cite.cited_by_article_id = nil
    assert !@cite.valid?

    @cite.cited_by_article_id = -2
    assert !@cite.valid?

    @cite.cited_by_article_id = 5.6
    assert !@cite.valid?
  end

  def test_bad_values_for_cite_order
    @cite = Cite.new(@mycite)

    # Checking for cite_order constraints
    @cite.cite_order = nil
    assert !@cite.valid?

    @cite.cite_order = -2
    assert !@cite.valid?

    @cite.cite_order = 5.6
    assert !@cite.valid?
  end

  def belongs_to_article
    @cite = Cite.new(@mycite)
    assert @cite.article.id, articles(:article1).id
    assert @cite.article.title, articles(:article1).title
    assert @cite.article.fpage, articles(:article1).fpage
    assert @cite.article.lpage, articles(:article1).lpage
    assert @cite.article.page_range, articles(:article1).page_range
    assert @cite.article.url, articles(:article1).url
    assert @cite.article.pacsnum, articles(:article1).pacsnum
    assert @cite.article.other, articles(:article1).other
  end

  def belongs_to_cite
    @cite = Cite.new(@mycite)
    assert @cite.cite.id, articles(:article5).id
    assert @cite.cite.title, articles(:article5).title
    assert @cite.cite.fpage, articles(:article5).fpage
    assert @cite.cite.lpage, articles(:article5).lpage
    assert @cite.cite.page_range, articles(:article5).page_range
    assert @cite.cite.url, articles(:article5).url
    assert @cite.cite.pacsnum, articles(:article5).pacsnum
    assert @cite.cite.other, articles(:article5).other
  end
end
