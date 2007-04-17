require File.dirname(__FILE__) + '/../test_helper'

class AuthorTest < Test::Unit::TestCase
  fixtures :authors
  
  def setup
    @authors = [:hector, :memo]
  end 

  # RIGHT
  def test_creating_authors_from_fixtures
    @authors.each { |author|
      @author = authors(author)
      assert @author.save
    }
  end
end
