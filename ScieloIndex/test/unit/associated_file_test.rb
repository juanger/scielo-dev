require File.dirname(__FILE__) + '/../test_helper'

class AssociatedFileTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues, :articles, :associated_files

  def setup
    @associated_files = [:art1files, :art2files]
    @myassociated_file = {:article_id => 3, :filename => 'n1231c123132', :pdf_path => 'PDF3', :html_path => 'HTML3' }
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_associated_files_from_fixtures
    @associated_files.each { |associated_file|
      @associated_file = associated_files(associated_file)
      @associated_file_db = AssociatedFile.find_by_filename(@associated_file.filename)
      assert_equal @associated_file.id, @associated_file_db.id
      assert_equal @associated_file.filename, @associated_file_db.filename
      assert_equal @associated_file.pdfdata, @associated_file_db.pdfdata
      assert_equal @associated_file.htmldata, @associated_file_db.htmldata
    }
   end

  def test_updating
      @associated_files.each { |associated_file|
      @associated_file = associated_files(associated_file)
      @associated_file_db = AssociatedFile.find_by_article_id(@associated_file.article_id)
      @associated_file_db.id = @associated_file_db.id
      assert @associated_file_db.update
      @associated_file_db.filename.reverse!
      assert @associated_file_db.update
      @associated_file_db.pdf_path.reverse!
      assert @associated_file_db.update
      @associated_file_db.html_path.reverse!
      assert @associated_file_db.update
    }
   end

  def test_deleting
    @associated_files.each { |associated_file|
      @associated_file = associated_files(associated_file)
      @associated_file_db = AssociatedFile.find_by_filename(@associated_file.filename)
      assert @associated_file_db.destroy
      @associated_file_db = AssociatedFile.find_by_filename(@associated_file.filename)
      assert @associated_file_db.nil?
    }
  end

  def test_creating_empty_associated_file
    @associated_file = AssociatedFile.new()
    assert !@associated_file.save
  end

  def test_uniqueness
    @associated_file = AssociatedFile.new(@myassociated_file)
    assert @associated_file.save
    @associated_file.article_id = associated_files(:art1files).article_id
    assert !@associated_file.save
  end

  # Boundary
  def test_bad_values_for_id
    @associated_file = AssociatedFile.new(@myassociated_file)

    # Checking for id constraints
    @associated_file.id = nil
    assert @associated_file.valid?

    @associated_file.id = -2
    assert !@associated_file.valid?

    @associated_file.id = 5.6
    assert !@associated_file.valid?
  end

  def test_bad_values_for_filename
    @associated_file = AssociatedFile.new(@myassociated_file)

    # Checking filename constraints
    @associated_file.filename = nil
    assert !@associated_file.valid?

    @associated_file.filename = "Fo"
    assert !@associated_file.valid?

    @associated_file.filename = "A"*201
    assert !@associated_file.valid?
  end
end
