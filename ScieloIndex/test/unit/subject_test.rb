require File.dirname(__FILE__) + '/../test_helper'

class SubjectTest < Test::Unit::TestCase
  fixtures :subjects

  def setup
    @subjects = [:geo, :atm, :bioqui, :fisica, :quimica]
    @mysubject = {:name => 'Quimica Inorganica', :parent_id => 4}
  end

  #RIGHT CRUD (Create, Update and Delete)
  def test_creating_subjects_from_fixtures
    @subjects.each { |subject|
      @subject = subjects(subject)
      @subject_db = Subject.find_by_name(@subject.name)
      assert_equal @subject.id, @subject_db.id
      assert_equal @subject.parent_id, @subject_db.parent_id
      assert_equal @subject.name, @subject_db.name
    }
  end

  def test_updating
    @subjects.each { |subject|
      @subject = subjects(subject)
      @subject_db = Subject.find_by_name(@subject.name)
      @subject_db.id = @subject_db.id
      assert @subject_db.update
      @subject_db.parent_id = @subject_db.parent_id
      assert @subject_db.update
      @subject_db.name.reverse
      assert @subject_db.update
    }
  end

  def test_deleting
    @subjects.each { |subject|
      @subject = subjects(subject)
      @subject_db = Subject.find_by_name(@subject.name)
      assert @subject_db.destroy
      @subject_db = Subject.find_by_name(@subject.name)
      assert @subject_db.nil?
    }
    @collection = Subject.find(:all)
    assert_equal 0, @collection.size
  end

  def test_creating_empty_subject
    @subject = Subject.new()
    assert !@subject.save
  end

  def test_checking_uniqueness
    @subject = Subject.new(@mysubject)
    @subject.name = 'Fisica'
    assert !@subject.save
  end

  # Boundary
  def test_bad_values_for_id
    @subject = Subject.new(@mysubject)

    # Checking for id constraints
    @subject.id = nil
    assert @subject.valid?

    @subject.id = 5.2
    assert !@subject.valid?

    @subject.id = -10
    assert !@subject.valid?
  end

  def test_bad_values_for_parent_id
    @subject = Subject.new(@mysubject)

    # Checking for parent_id constraints
    @subject.parent_id = nil
    assert @subject.valid?

    @subject.parent_id = 5.2
    assert !@subject.valid?

    @subject.parent_id = -10
    assert !@subject.valid?
  end

  def test_bad_values_for_name
    @subject = Subject.new(@mysubject)

    # Checking for name constraints
    @subject.name = nil
    assert !@subject.valid?

    @subject.name = "AA"
    assert !@subject.valid?

    @subject.name = "A"*501
    assert !@subject.valid?
  end
end
