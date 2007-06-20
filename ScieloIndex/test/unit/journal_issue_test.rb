require File.dirname(__FILE__) + '/../test_helper'

class JournalIssueTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues

  def setup
    @journal_issues = [:atm19_1, :csalud1_10]
    @myjournal_issue = {:journal_id => 2, :title => 'Santo contra las momias', :number => '19', :volume => '1', :year => 2006}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_journal_issues_from_fixtures
    @journal_issues.each { |journal_issue|
      @journal_issue = journal_issues(journal_issue)
      @journal_issue_db = JournalIssue.find_by_journal_id_and_number_and_volume_and_year(@journal_issue.journal_id, @journal_issue.number, @journal_issue.volume, @journal_issue.year)
      assert_equal @journal_issue.id, @journal_issue_db.id
      assert_equal @journal_issue.journal_id, @journal_issue_db.journal_id
      assert_equal @journal_issue.number, @journal_issue_db.number
      assert_equal @journal_issue.volume, @journal_issue_db.volume
      assert_equal @journal_issue.year, @journal_issue_db.year
    }
  end

  def test_updating
    @journal_issues.each { |journal_issue|
      @journal_issue = journal_issues(journal_issue)
      @journal_issue_db = JournalIssue.find_by_journal_id_and_number_and_volume_and_year(@journal_issue.journal_id, @journal_issue.number, @journal_issue.volume, @journal_issue.year)
      @journal_issue_db.id = @journal_issue_db.id
      assert @journal_issue_db.update
      @journal_issue_db.journal_id = @journal_issue_db.journal_id
      assert @journal_issue_db.update
      @journal_issue_db.number.reverse!
      assert @journal_issue_db.update
      @journal_issue_db.volume.reverse!
      assert @journal_issue_db.update
      @journal_issue_db.year = @journal_issue_db.year
      assert @journal_issue_db.update
    }
  end

  def test_deleting
    @journal_issues.each { |journal_issue|
      @journal_issue = journal_issues(journal_issue)
      @journal_issue_db = JournalIssue.find_by_journal_id_and_number_and_volume_and_year(@journal_issue.journal_id, @journal_issue.number, @journal_issue.volume, @journal_issue.year)
      assert @journal_issue_db.destroy
      @journal_issue_db = JournalIssue.find_by_number_and_volume_and_year(@journal_issue.number, @journal_issue.volume, @journal_issue.year)
      assert @journal_issue_db.nil?
    }
  end

  def test_creating_empty_journal_issue
    @journal_issue = JournalIssue.new()
    assert !@journal_issue.save
  end

  def test_uniqueness
    @journal_issue = JournalIssue.new(@myjournal_issue)
    assert @journal_issue.save
    @journal_issue.journal_id = '1'
    assert !@journal_issue.save
  end

  # Boundary
  def test_bad_values_for_id
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for ID constraints
    @journal_issue.id = nil
    assert @journal_issue.valid?

    @journal_issue.id = -2
    assert !@journal_issue.valid?

    @journal_issue.id = 5.6
    assert !@journal_issue.valid?
  end

  def test_bad_values_for_journal_id
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for journal_id constraints
    @journal_issue.journal_id = nil
    assert !@journal_issue.valid?

    @journal_issue.journal_id = -2
    assert !@journal_issue.valid?

    @journal_issue.journal_id = 5.6
    assert !@journal_issue.valid?
  end

   def test_bad_values_for_title
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for volume constraints
    @journal_issue.title = nil
    assert @journal_issue.valid?

    @journal_issue.title = ""
    assert @journal_issue.valid?

    @journal_issue.title = "A"*201
    assert !@journal_issue.valid?
  end

  def test_bad_values_for_supplement
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for volume constraints
    @journal_issue.supplement = nil
    assert @journal_issue.valid?

    @journal_issue.supplement = ""
    assert @journal_issue.valid?

    @journal_issue.supplement = "A"*101
    assert !@journal_issue.valid?
  end


  def test_bad_values_for_number
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for number constraints
    @journal_issue.number = nil
    assert @journal_issue.valid?

    @journal_issue.number = ""
    assert @journal_issue.valid?

    @journal_issue.number = "A"*101
    assert !@journal_issue.valid?
  end

  def test_bad_values_for_volume
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for volume constraints
    @journal_issue.volume = nil
    assert @journal_issue.valid?

    @journal_issue.volume = ""
    assert @journal_issue.valid?

    @journal_issue.volume = "A"*101
    assert !@journal_issue.valid?
  end

    def test_incompleteness
    @journal_issue = JournalIssue.new(@myjournal)
    @journal_issue.save()

    assert_equal  false, @journal_issue.incomplete

    @journal_issue.incomplete = true
    @journal_issue.save()

    assert_equal true, @journal_issue.incomplete
  end

  def test_bad_values_for_year
    @journal_issue = JournalIssue.new(@myjournal_issue)

    # Checking for year constraints
    @journal_issue.year = nil
    assert !@journal_issue.valid?

    @journal_issue.year = -1
    assert !@journal_issue.valid?

    @journal_issue.year = 2007.2
    assert !@journal_issue.valid?

    @journal_issue.year = Date.today.year - 1001
    assert !@journal_issue.valid?

    @journal_issue.year = Date.today.year + 2
    assert !@journal_issue.valid?
  end

  def test_belongs_to_journal
    @journal_issue = journal_issues(:atm19_1)
    @journal = journals(:atmosfera)
    assert_equal @journal_issue.journal.title, @journal.title
    assert_equal @journal_issue.journal.country_id, @journal.country_id
    assert_equal @journal_issue.journal.url, @journal.url
  end

  def test_has_many_articles
    @journal_issue = journal_issues(:atm19_1)
    assert_equal @journal_issue.articles.size, 5
  end
end
