require File.dirname(__FILE__) + '/../test_helper'

class JournalIssueTest < Test::Unit::TestCase
  fixtures :journals
  fixtures :journal_issues

  def setup
    @journal_issues = [:atmosfera, :csalud]
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
      @journal_issue_db.journal_id = @journal_issue_db.journal_id + 1
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

  # Boundary
  def test_bad_values_for_id
    @journal_issue = JournalIssue.new({:id => 1, :journal_id => 2, :number => '19', :volume => '1', :year => 2006})
    
    # Checking for ID constraints
    @journal_issue.id = nil
    assert @journal_issue.valid?
  end
  
  def test_bad_values_for_fields_nil
    @journal_issue = JournalIssue.new({:id => 1, :journal_id => 2, :number => '19', :volume => '1', :year => 2006})
    
     # Checking title constraints
    @journal_issue.number = nil
    assert !@journal_issue.valid?
    
    @journal_issue.volume = nil
    assert !@journal_issue.valid?
     
    @journal_issue.year = nil
    assert !@journal_issue.valid?  
  end
end
