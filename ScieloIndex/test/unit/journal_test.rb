require File.dirname(__FILE__) + '/../test_helper'

class JournalTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues

  def setup
    @journals = [:atmosfera, :csalud, :octopus]
    @myjournal = {:title => 'Technology Review', :country_id => 840, :publisher_id => 1, :state => 'Texas', :city => 'Houston', :other => 'For ultra cool kids', :issn => '1234-1235'}
  end

  # RIGHT
  def test_creating_journals_from_fixtures
    @journals.each { |journal|
      @journal = journals(journal)
      @journal_db = Journal.find_by_title(@journal.title)
      assert_equal @journal.id, @journal_db.id
      assert_equal @journal.title, @journal_db.title
      assert_equal @journal.publisher_id, @journal_db.publisher_id
      assert_equal @journal.state, @journal_db.state
      assert_equal @journal.city, @journal_db.city
      assert_equal @journal.country_id, @journal_db.country_id
      assert_equal @journal.url, @journal_db.url
      assert_equal @journal.email, @journal_db.email
      assert_equal @journal.abbrev, @journal_db.abbrev
      assert_equal @journal.issn, @journal_db.issn
      assert_equal @journal.other, @journal_db.other
    }
  end

  def test_updating
    @journals.each { |journal|
      @journal = journals(journal)
      @journal_db = Journal.find_by_title(@journal.title)
      @journal_db.title.reverse!
      assert @journal_db.update
      @journal_db.id = @journal_db.id
      assert @journal_db.update
      @journal_db.country_id = @journal_db.country_id + 1
      assert @journal_db.update
      @journal_db.publisher_id = @journal_db.publisher_id + 1
      assert @journal_db.update
      @journal_db.state.reverse!
      assert @journal_db.update
      @journal_db.city.reverse!
      assert @journal_db.update
      @journal_db.url.reverse!
      assert @journal_db.update
      @journal_db.other.reverse!
      assert @journal_db.update
      @journal_db.email.reverse!
      assert @journal_db.update
      @journal_db.abbrev.reverse!
      assert @journal_db.update
      @journal_db.issn.reverse!
      assert @journal_db.update
    }
  end

  def test_deleting
    @journals.each { |journal|
      @journal = journals(journal)
      @journal_db = Journal.find_by_title(@journal.title)
      assert @journal_db.destroy
      @journal_db = Journal.find_by_title(@journal.title)
      assert @journal_db.nil?
    }
  end

  def test_creating_empty_journal
    @journal = Journal.new()
    assert !@journal.save
  end

  def test_checking_uniqueness
    @journal = Journal.new(@myjournal)
    @journal.issn = '1234-1234'

    assert !@journal.save
  end

  # Boundary
  def test_bad_values_for_id
    @journal = Journal.new(@myjournal)

    # Checking for ID constraints
    @journal.id = nil
    assert @journal.valid?

    @journal.id = -2
    assert !@journal.valid?

    @journal.id = 5.6
    assert !@journal.valid?
  end

  def test_bad_values_for_title
    @journal = Journal.new(@myjournal)

    # Checking title constraints
    @journal.title = nil
    assert !@journal.valid?

    @journal.title = "Fo"
    assert !@journal.valid?

    @journal.title = "A"*101
    assert !@journal.valid?

    @journal.title = "Lars1"
    assert !@journal.valid?
  end

  def test_bad_values_for_publisher_id
    @journal = Journal.new(@myjournal)

    # Checking publisher_id constraints
    @journal.publisher_id = nil
    assert !@journal.valid?

    @journal.publisher_id = 10000
    assert !@journal.valid?

    @journal.publisher_id = -2
    assert !@journal.valid?

    @journal.publisher_id = 2.5
    assert !@journal.valid?
  end

  def test_bad_values_for_state
    @journal = Journal.new(@myjournal)

    # Checking state constraints
    @journal.state = nil
    assert @journal.valid?

    @journal.state = ""
    assert @journal.valid?

    @journal.state = "A"*31
    assert !@journal.valid?

    @journal.state = "Texas1"
    assert !@journal.valid?
  end

  def test_bad_values_for_city
    @journal = Journal.new(@myjournal)

    # Checking city constraints
    @journal.city = nil
    assert @journal.valid?

    @journal.city = ""
    assert @journal.valid?

    @journal.city = "A"*31
    assert !@journal.valid?

    @journal.city = "Las Vegas1"
    assert !@journal.valid?
  end

  def test_bad_values_for_country_id
    @journal = Journal.new(@myjournal)

    # Checking country_id constraints
    @journal.country_id = nil
    assert !@journal.valid?

    @journal.country_id = 1000
    assert !@journal.valid?

    @journal.country_id = -2
    assert !@journal.valid?

    @journal.country_id = 10.6
    assert !@journal.valid?
  end

  def test_bad_values_for_url
    @journal = Journal.new(@myjournal)

    # Checking url constraints
    @journal.url = nil
    assert @journal.valid?

    @journal.url = ""
    assert @journal.valid?

    @journal.url = "A"*201
    assert !@journal.valid?

    @journal.url = "http://[]"
    assert !@journal.valid?
  end

  def test_bad_values_for_email
    @journal = Journal.new(@myjournal)

    # Checking email constraints
    @journal.email = nil
    assert @journal.valid?

    @journal.email = ""
    assert @journal.valid?

    @journal.email = "A"*21
    assert !@journal.valid?

    @journal.email = "foo@[].com"
    assert !@journal.valid?
  end

  def test_bad_values_for_abbrev
    @journal = Journal.new(@myjournal)

    # Checking abbrev constraints
    @journal.abbrev = nil
    assert @journal.valid?

    @journal.abbrev = ""
    assert @journal.valid?

    @journal.abbrev = "A"*21
    assert !@journal.valid?

    @journal.abbrev = "FOO[]"
    assert !@journal.valid?
  end

  def test_bad_values_for_issn
    @journal = Journal.new(@myjournal)

    # Checking issn constraints
    @journal.issn = nil
    assert @journal.valid?

    @journal.issn = ""
    assert @journal.valid?

    @journal.issn = "A"*10
    assert !@journal.valid?

    @journal.issn = "1234-123#"
    assert !@journal.valid?
  end

  def test_bad_values_for_other
    @journal = Journal.new(@myjournal)

    # Checking other constraints
    @journal.other = nil
    assert @journal.valid?

    @journal.other = ""
    assert @journal.valid?

    @journal.other = "A"*201
    assert !@journal.valid?

    @journal.other = "Cool kinds like comics |foo|"
    assert !@journal.valid?
  end

  def test_has_many_journal_issues
    @journal = Journal.find(1)
    assert_equal @journal.journal_issues.first.id, 1
    assert_equal @journal.journal_issues.first.number, '19'
    assert_equal @journal.journal_issues.first.year, 2006
  end
end
